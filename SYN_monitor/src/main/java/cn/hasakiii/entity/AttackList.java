package cn.hasakiii.entity;

import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableId;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

/**
 * @Author: Z.Richard
 * @CreateTime: 2021/1/7 15:00
 * @Description:
 **/
@Data
@AllArgsConstructor
@NoArgsConstructor
public class AttackList {
    @TableId(type = IdType.AUTO)
    private Integer id;
    private String attack;

    public AttackList(String attack) {
        this.attack = attack;
    }
}
