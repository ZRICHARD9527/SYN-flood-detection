package cn.hasakiii.util;

import java.util.List;

/**
 * @Author: Z.Richard
 * @CreateTime: 2021/1/7 10:31
 * @Description:
 **/

public class SYNUtil {

    private static int MAX_SYN = 2;//监测阈值
    private static int lastJudge = 0;//最近一次判断

    //监测是否受到攻击，返回信息
    public native AttackInfo getStruct(AttackInfo attackInfo, String ip, int port, int maxsyn);


    public static AttackInfo getAttackInfo(String ip, int port) {
        System.loadLibrary("SYNUtil");
        SYNUtil synUtil = new SYNUtil();
        return synUtil.getStruct(new AttackInfo(), ip, port, MAX_SYN);
    }

    //攻击判断，连续两次之内和大于1认为受到攻击
    public static boolean ifAttacked(String ip, int port) {
        AttackInfo at = getAttackInfo(ip, port);
        int judge = at.getIfAttacked() + lastJudge;
        if (judge > 0) {
            return true;
        } else {
            return false;
        }
    }

    public static void main(String[] args) {
        System.loadLibrary("SYNUtil");
        while (true) {
            System.out.println("***************************************************************");
            SYNUtil synUtil = new SYNUtil();
            AttackInfo attackInfo = synUtil.getStruct(new AttackInfo(), "10.151.98.231", 80, 5);
            System.out.println("是否受到攻击 ： " + attackInfo.getIfAttacked());
            System.out.println("攻击者IP ： " + attackInfo.getAttackIP());
            System.out.println("攻击数量 ：" + attackInfo.getInvalidSYN());
        }
    }
}
