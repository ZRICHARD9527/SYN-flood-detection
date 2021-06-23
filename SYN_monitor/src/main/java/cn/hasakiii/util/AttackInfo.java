package cn.hasakiii.util;


/**
 * @Author: Z.Richard
 * @CreateTime: 2021/1/7 10:34
 * @Description:
 **/

public class AttackInfo {
    private int ifAttacked;//是否受到攻击
    private int invalidSYN;//监测到的无效SYN数量
    private String attackIP;//疑似攻击IP

    public AttackInfo() {
    }

    public AttackInfo(int ifAttacked, int invalidSYN, String attackIP) {
        this.ifAttacked = ifAttacked;
        this.invalidSYN = invalidSYN;
        this.attackIP = attackIP;
    }

    public int getIfAttacked() {
        return ifAttacked;
    }

    public void setIfAttacked(int ifAttacked) {
        this.ifAttacked = ifAttacked;
    }

    public int getInvalidSYN() {
        return invalidSYN;
    }

    public void setInvalidSYN(int invalidSYN) {
        this.invalidSYN = invalidSYN;
    }

    public String getAttackIP() {
        return attackIP;
    }

    public void setAttackIP(String attackIP) {
        this.attackIP = attackIP;
    }
}
