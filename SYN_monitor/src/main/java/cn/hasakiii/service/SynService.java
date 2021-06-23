package cn.hasakiii.service;

import cn.hasakiii.entity.AttackList;
import cn.hasakiii.mapper.AttackListMapper;
import cn.hasakiii.result.ResultModel;
import cn.hasakiii.util.AttackInfo;
import cn.hasakiii.util.SYNUtil;
import org.mybatis.spring.annotation.MapperScan;
import org.springframework.stereotype.Service;

import javax.annotation.Resource;

/**
 * @Author: Z.Richard
 * @CreateTime: 2021/1/7 14:57
 * @Description:
 **/
@Service
//@MapperScan(value = {"cn.hasakiii.mapper"})
public class SynService {
    private int lastJudge = 0;

    @Resource
    private AttackListMapper attackListMapper;

    //判断是否有syn攻击
    public ResultModel test(String ip, Integer port) {
        AttackInfo at = SYNUtil.getAttackInfo(ip, port);

        if (at.getIfAttacked() + lastJudge > 0) {
            lastJudge = at.getIfAttacked();
            //ip字符串解析后加入数据库
            String[] ipArr = at.getAttackIP().split("/");
            for (int i = 0; i < ipArr.length; ++i) {
                insert(ipArr[i]);
            }

            return new ResultModel(2, ip + " : " + port + "\t受到攻击", at);
        }
        return new ResultModel(1, "没有受到攻击", at);
    }

    //将攻击ip存进数据库
    public boolean insert(String ip) {
        int i = attackListMapper.insert(new AttackList(ip));
        return i >= 0;
    }
}
