#include "pch.h"
#include "SYNUtil.h"
#include "cn_hasakiii_util_SYNUtil.h"

#define _CRT_SECURE_NO_WARNINGS  //避免因为strcpy,scanf等不安全的函数，而报警告和错误，导致无法编译通过
#define _WINSOCK_DEPRECATED_NO_WARNINGS

#include <Winsock2.h>
#include <iostream>
#include "pcap.h"
#include "stdio.h"
#include <time.h>
#include <string>
#include <winpackagefamily.h>
#include <fstream>  //文件的输入输出;
#pragma comment(lib,"ws2_32.lib")
#pragma comment(lib,"wpcap.lib")
using namespace std;

//解析信息
typedef struct DeInfo
{
    unsigned char* srcMac;//源mac地址
    unsigned char* dstMac;//目的mac地址
    string srcIP;         //源ip
    string dstIP;         //目的ip
    int srcport;          //源端口
    int dstport;          //目的端口
    int flag;             //标志，0 syn；1 ack
    string protocol;      //应用层协议类型
    int size;             //数据长度
    long long ack;        //当前的确认序号
}DeInfo;

//用来保存每次从网卡中读取的信息
DeInfo info;


string listenIp;//监听的ip
int listenPort = -1;//监听的端口，-1为监听所有
int onlySynCount = 0;//无效的syn包数
long long  lastResponseSeq = 0;//最近一次响应seq序列值
int maxsyn=0;//无效syn阈值
string attackip;//攻击ip

                   
                   
//以太网帧报头协议格式 
struct ethernet_header
{
    u_int8_t ether_dhost[6];  /*目的以太地址,48位*/
    u_int8_t ether_shost[6];  /*源以太网地址,48位*/
    u_int16_t ether_type;     /*以太网类型,2字节，标识数据交付哪个协议处理。例如，字段为 0x0800 时，表示将数据交付给 IP 协议*/
};

//ip地址格式,32位无符号整型表示
typedef u_int32_t in_addr_t;

//IP数据报头部格式
struct ip_header
{
    // 位域以字节为单位

#ifdef WORKS_BIGENDIAN          /*字节序*/
    u_int8_t ip_version : 4,     /*version:4*/
        ip_header_length : 4;   /*IP协议首部长度Header Length*/
#else
    u_int8_t ip_header_length : 4,//低位
        ip_version : 4;           //高位
#endif
    u_int8_t ip_tos;         /*服务类型Differentiated Services  Field*/
    u_int16_t ip_length;     /*总长度Total Length，总长度指首部和数据之和的长度，单位为字节。总长度字段为16位，因此数据报的最大长度为216-1=65535字节*/
    u_int16_t ip_id;         /*标识identification，用于分片重组*/
    u_int16_t ip_off;        /*片偏移*/
    u_int8_t ip_ttl;         /*生存时间Time To Live*/
    u_int8_t ip_protocol;    /*协议类型（TCP或者UDP协议）6：TCP   17：UDP  88：IGRP   89：OSPF */
    u_int16_t ip_checksum;   /*首部检验和，只对IP头部的正确性进行检验*/
    struct in_addr  ip_source_address; /*源IP*/
    struct in_addr  ip_destination_address; /*目的IP*/
};

//关于tcp数据报头部格式
struct tcp_header
{
    u_int16_t tcp_source_port;		  //源端口号
    u_int16_t tcp_destination_port;	  //目的端口号
    u_int32_t tcp_acknowledgement;	  //序号
    u_int32_t tcp_ack;	//确认号字段
#ifdef WORDS_BIGENDIAN
    u_int8_t tcp_offset : 4,
        tcp_reserved : 4;
#else
    u_int8_t tcp_reserved : 4,//保留
        tcp_offset : 4;//数据偏移
#endif
    u_int8_t tcp_flags;
    u_int16_t tcp_windows;	//窗口字段
    u_int16_t tcp_checksum;	//检验和
    u_int16_t tcp_urgent_pointer;	//紧急指针字段
};


//打印信息
void printInfo() {
    if (listenPort != -1) {
        //如果监听端口不为-1则判断收到端口是否为监听端口
        if (info.srcport != listenPort && info.dstport != listenPort) {
            return;
        }
    }
    /*cout << "===================================数据解析====================================\n" << endl;
    printf("源MAC地址  :%02x:%02x:%02x:%02x:%02x:%02x:\n", *info.srcMac, *(info.srcMac + 1), *(info.srcMac + 2), *(info.srcMac + 3), *(info.srcMac + 4), *(info.srcMac + 5));
    printf("目的MAC地址:%02x:%02x:%02x:%02x:%02x:%02x:\n", *info.dstMac, *(info.dstMac + 1), *(info.dstMac + 2), *(info.dstMac + 3), *(info.dstMac + 4), *(info.dstMac + 5));
    cout << "源IP地址   :" + info.srcIP + "\t 源端口  : \t" << info.srcport << endl;
    cout << "目的IP地址 :" + info.dstIP + "\t 目的端口: \t" << info.dstport << endl;
    cout << "数据长度   :" << info.size << endl;
    cout << "应用协议类型:" + info.protocol << endl;
    printf("FLAGS      :");*/


    if (info.flag & 0x08) {
        //printf("【推送 PSH】");
    }
    if (info.flag & 0x10) {
        //printf("【确认 ACK】 ");
        if (info.srcIP.compare(listenIp) != 0) {
            //源地址不是本机，客户端发包,判断seq值是否为连接成功
            if (info.ack == lastResponseSeq + 1) {
                //说明连接成功，不是无效syn
                onlySynCount--;
            }
        }
    }
    if (info.flag & 0x02) {
        //printf("【同步 SYN】");
        if (info.srcIP.compare(listenIp) != 0) {
            //源地址不是本机，此种情况下请求方不是本机才会在响应阶段加一
            onlySynCount++;
            if (info.flag & 0x10) {
                //本机请求其他连接时收到的ack+syn
                onlySynCount--;
            }
        }
    }
    if (info.flag & 0x20) {
    }
    if (info.flag & 0x01) {
        
    }if (info.flag & 0x04) {
    }

    if (onlySynCount >= maxsyn) {
        if (info.srcIP.compare(listenIp) == 0) {
            attackip += info.dstIP + "/";
        }
        else {
            attackip += info.srcIP + "/";
        }
        return;
    }

}



//实现tcp数据包分析
void tcp_protocol_packet_callback(u_char* argument,
    const struct pcap_pkthdr* packet_header,
    /*
    * libpcap捕获时，使用pcap_loop之类的函数，在调用处理的handle的时候，
    * 返回的第一个参数的类型为pcap_pkthdr,该结构体包含三个属性，
    * ts：时间戳  cpalen：当前分组的长度（真正实际捕获的包的长度） len：数据包的长度
    */
    const u_char* packet_content)
{
    struct tcp_header* tcp_protocol; /*tcp协议变量*/
    u_char flags;                    /*标记*/
    int header_length;               /*头长度*/
    u_short source_port;             /*源端口*/
    u_short destination_port;        /*目的端口*/
    u_short windows;                 /*窗口大小*/
    u_short urgent_pointer;          /*紧急指针*/
    u_int sequence;                  /*序列号*/
    u_int acknowledgement;           /*确认号*/
    u_int16_t checksum;              /*检验和*/
    tcp_protocol = (struct tcp_header*)(packet_content + 14 + 20);  /*获得tcp首部内容，以太网协议报头长度为14字节，IP头部为20字节*/

    /*
    * ntohs()将一个16位数由网络字节顺序转换为主机字节顺序,
    * 因为主机字节顺序是从高到低
    * htons()表示将16位的主机字节顺序转化为16位的网络字节顺序（ip地址是32位的端口号是16位的 ）
    * htonl()表示将32位的主机字节顺序转化为32位的网络字节顺序
    */

    source_port = ntohs(tcp_protocol->tcp_source_port);             /*获得源端口号*/
    destination_port = ntohs(tcp_protocol->tcp_destination_port);   /*获得目的端口号*/
    header_length = tcp_protocol->tcp_offset * 4;                   /*获得首部长度*/
    sequence = ntohl(tcp_protocol->tcp_acknowledgement);            /*获得序列号*/
    acknowledgement = ntohl(tcp_protocol->tcp_ack);
    windows = ntohs(tcp_protocol->tcp_windows);
    urgent_pointer = ntohs(tcp_protocol->tcp_urgent_pointer);
    flags = tcp_protocol->tcp_flags;
    checksum = ntohs(tcp_protocol->tcp_checksum);

    int min = (destination_port < source_port) ? destination_port : source_port;
    switch (min)
    {
    case 80:
        info.protocol = "HTTP";
        break;
    case 21:
        info.protocol = "FTP";
        break;

    case 23:
        info.protocol = "Telnet";
        break;

    case 25:
        info.protocol = "SMTP";
        break;

    case 110:
        info.protocol = "POP3";
        break;
    case 443:
        info.protocol = "HTTPS";
        break;
    default:
        info.protocol = "其他类型";
        break;
    }

    //cout << endl;
    //printf("序列号：\t %u \n", sequence);
    //printf("确认号：\t %u \n", acknowledgement);
    //printf("首部长度：\t %d \n", header_length);
    //printf("保留字段：\t %d \n", tcp_protocol->tcp_reserved);
    //printf("flag标志：\t %u \n", flags);
    //printf("控制位：");
    if ((flags & 0x10) && (flags & 0x02)) {
        //如果同时有ack和syn说明为响应，则记录seq值
        lastResponseSeq = sequence;
    }

    info.srcport = source_port;
    info.dstport = destination_port;
    info.flag = flags;
    info.ack = acknowledgement;
    printInfo();
    //printf("\n");
    //printf("窗口大小 :\t%d \n", windows);
    //printf("检验和 :\t%d\n", checksum);
    //printf("紧急指针字段 :\t%d\n", urgent_pointer);
}



/*下边实现IP数据包分析的函数定义ethernet_protocol_packet_callback*/
void ip_protocol_packet_callback(u_char* argument, const struct pcap_pkthdr*
    packet_header, const u_char* packet_content)
{
    struct ip_header* ip_protocol;  /*ip协议变量*/
    u_int   header_length;          /*长度*/
    u_int   offset;                 /*片偏移*/
    u_char  tos;                    /*服务类型*/
    u_int16_t checksum;             /*首部检验和*/
    ip_protocol = (struct ip_header*)(packet_content + 14); /*获得ip数据包的内容去掉以太头部*/
    checksum = ntohs(ip_protocol->ip_checksum);         /*获得校验和*/
    header_length = ip_protocol->ip_header_length * 4; /*获得长度*/
    tos = ip_protocol->ip_tos;                         /*获得tos*/
    offset = ntohs(ip_protocol->ip_off);               /*获得偏移量*/

    //printf("\n########################网络层IP协议######################## \n");
    //printf("IP版本:\t\tIPv%d\n", ip_protocol->ip_version);
    //printf("IP协议首部长度:\t%d\n", header_length);
    //printf("服务类型:\t%d\n", tos);
    //printf("总长度:\t\t%d\n", ntohs(ip_protocol->ip_length));/*获得总长度*/
    //printf("标识:\t\t%d\n", ntohs(ip_protocol->ip_id));      /*获得标识*/
    //printf("片偏移:\t\t%d\n", (offset & 0x1fff) * 8);        /**/
    //printf("生存时间:\t%d\n", ip_protocol->ip_ttl);          /*获得ttl*/
    //printf("首部检验和:\t%d\n", checksum);

    //printf("源IP:\t%s\n", inet_ntoa(ip_protocol->ip_source_address));    /*获得源ip地址*/
    //printf("目的IP:\t%s\n", inet_ntoa(ip_protocol->ip_destination_address));/*获得目的ip地址*/
    //printf("协议号:\t%d\n", ip_protocol->ip_protocol);         /*获得协议类型*/

    info.srcIP = (const char*)inet_ntoa(ip_protocol->ip_source_address);
    info.dstIP = (const char*)inet_ntoa(ip_protocol->ip_destination_address);

    //cout << "\n传输层协议是:\t";
    switch (ip_protocol->ip_protocol)
    {
    case 6:
        //printf("TCP\n");
        if (listenIp == inet_ntoa(ip_protocol->ip_source_address) || listenIp == inet_ntoa(ip_protocol->ip_destination_address)) {
            tcp_protocol_packet_callback(argument, packet_header, packet_content);
        }
        break; /*协议类型是6代表TCP*/
    case 17:
        //printf("UDP\n");
        break;/*17代表UDP*/
    case 1:
        //printf("ICMP\n");
        break;/*代表ICMP*/
    case 2:
        //printf("IGMP\n");
        break;/*代表IGMP*/
    default:break;
    }
}

/*
* 解析以太网数据帧
*/
void ethernet_protocol_packet_callback(u_char* argument, const struct pcap_pkthdr* packet_header, const u_char* packet_content)
{
    u_short ethernet_type;                      /*以太网协议类型*/
    struct ethernet_header* ethernet_protocol;  /*以太网协议变量*/
    //u_char* mac_string;
    static int packet_number = 1;
    //printf("\n**********************************************\n");
    //printf("\n            第【%d】个以太网数据帧            \n", packet_number);
    //printf("\n**************链路层以太网协议****************\n");
    ethernet_protocol = (struct ethernet_header*)packet_content;/*获得一太网协议数据内容*/
    //printf("数据长度 :\t %d\n", packet_header->caplen);
    //printf("以太网类型为 :\t");
    ethernet_type = ntohs(ethernet_protocol->ether_type); /*获得以太网类型*/
    //printf("%04x\n", ethernet_type);

    /*获得mac源地址*/
    //printf("mac源地址:\t");
    //mac_string = ethernet_protocol->ether_shost;
    //输出用16进制表示，至少两位，不够补零
    //printf("%02x:%02x:%02x:%02x:%02x:%02x:\n", *mac_string, *(mac_string + 1), *(mac_string + 2), *(mac_string + 3), *(mac_string + 4), *(mac_string + 5));

    /*获得mac目的地址*/
    //printf("mac目的地址:\t");
    //mac_string = ethernet_protocol->ether_dhost;
    //printf("%02x:%02x:%02x:%02x:%02x:%02x:\n", *mac_string, *(mac_string + 1), *(mac_string + 2), *(mac_string + 3), *(mac_string + 4), *(mac_string + 5));

    info.srcMac = ethernet_protocol->ether_shost;
    info.dstMac = ethernet_protocol->ether_dhost;
    info.size = packet_header->caplen;

    switch (ethernet_type)
    {
    case 0x0800:
        /*如果上层是ipv4ip协议,就调用分析ip协议的函数对ip包进行解析*/
        ip_protocol_packet_callback(argument, packet_header, packet_content);
        break;
    case 0x0806:
    case 0x8035:
    default:break;
    }
    packet_number++;
}


/*
* 获取网卡列表{网卡描述}
*/



JNIEXPORT jobject JNICALL Java_cn_hasakiii_util_SYNUtil_getStruct
(JNIEnv* env , jobject instance, jobject attackInfo, jstring ip, jint port, jint synnum){
    
    listenIp = (char*)env->GetStringUTFChars(ip, 0);
    listenPort = port;
    maxsyn = synnum;

    pcap_if_t* alldevs;
    /*
    * pcap_if_t 是 pcap_if 的别名
    * pcap_if是winpcap这个抓包框架中自带的函数库中的主要函数之一,用来描述一个网络设备结构
    *
    * pcap_if * next
    * 如果为空，当前元素为列表最后一个元素，如果非空，指向列表的下一个元素；
    * char * name
    * 如果非空，表示函数pcap_open_live()返回值为真的设备名字；
    * char * description
    * 如果非空，表示设备描述；
    * pcap_addr * addresses
    * 表示接口地址
    * u_int flags
    * 当设备接口是回环接口时flag置1
    * PCAP_IF_ interface flags. Currently the only possible flag is PCAP_IF_LOOPBACK, that is set if the interface is a loopback interface.
    */
    pcap_if_t* d;
    int inum = 2;
    pcap_t* adhandle;
    char errbuf[PCAP_ERRBUF_SIZE];//错误缓冲区大小
    int cnt = 15;//抓包数,-1表示一直抓

    /* 获得网卡的列表 */
    if (pcap_findalldevs(&alldevs, errbuf) == -1)
    {
        fprintf(stderr, "Error in pcap_findalldevs: %s\n", errbuf);
        //exit(1);
    }

    int i = 0;
    /* 找到要选择的网卡结构 */
    for (d = alldevs, i = 0; i < inum - 1; d = d->next, i++);

    /* 打开选择的网卡 */
    if ((adhandle = pcap_open_live(d->name, /* 设备名称*/
        65536,       /* 最大值.*/
                     /*65536允许整个包在所有mac电脑上被捕获.*/
        1,           /* 混杂模式*/
        /*
        混杂模式是指一台主机能够接受所有经过它的数据流，
        不论这个数据流的目的地址是不是它，它都会接受这个数据包。
        也就是说，混杂模式下，网卡会把所有的发往它的包全部都接收。
        在这种情况下，可以接收同一集线器局域网的所有数据。
        */
        1000,   /* 读超时为1秒*/
        errbuf  /* error buffer*/
    )) == NULL)
    {//无法打开
        pcap_freealldevs(alldevs);
    }

    pcap_freealldevs(alldevs);
   
    /* 开始以回调的方式捕获包
      函数名称：int pcap_loop(pcap_t * p,int cnt, pcap_handler callback, uchar * user);
      函数功能：捕获数据包,不会响应pcap_open_live()函数设置的超时时间
      */
    pcap_loop(adhandle, cnt, ethernet_protocol_packet_callback, NULL);


    jclass myClass = env->FindClass("cn/hasakiii/util/AttackInfo");

    // 对应的Java属性
    jfieldID attackIP = env->GetFieldID(myClass, "attackIP", "Ljava/lang/String;");
    jfieldID invalidSYN = env->GetFieldID(myClass, "invalidSYN", "I");
    jfieldID ifAttacked = env->GetFieldID(myClass, "ifAttacked", "I");

    //属性赋值，person为传入的Java对象
    env->SetObjectField(attackInfo, attackIP, env->NewStringUTF(attackip.c_str()));
    env->SetIntField(attackInfo, invalidSYN, onlySynCount);

    if (onlySynCount>maxsyn) {
        //受到攻击
        env->SetIntField(attackInfo, ifAttacked, 1);
    }
    else {
        env->SetIntField(attackInfo, ifAttacked, 0);
    }

    //计数清空
    /*
     * 为了防止受到攻击而抓包间隔太短，导致每次抓包间隔内都不满而误判
     * 此时则不清空，只有当受到攻击后才清空重新计数
     */
    if (onlySynCount > (maxsyn)) {
        onlySynCount = 0;
        attackip = "";
    }
    return attackInfo;
}