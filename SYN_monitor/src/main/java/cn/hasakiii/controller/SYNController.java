package cn.hasakiii.controller;

import cn.hasakiii.result.ResultModel;
import cn.hasakiii.service.SynService;
import com.alibaba.fastjson.JSONObject;
import org.springframework.web.bind.annotation.*;

import javax.annotation.Resource;

/**
 * @Author: Z.Richard
 * @CreateTime: 2021/1/7 14:31
 * @Description:
 **/

@RestController
public class SYNController {

    @Resource
    SynService synService;

    //判断是否受到攻击
    @PostMapping("/test")
    public ResultModel test(@RequestBody JSONObject jsonObject) {

        System.out.println(jsonObject.toString());
        String ip = String.valueOf(jsonObject.get("ip")).trim();
        Integer port = Integer.valueOf(String.valueOf(jsonObject.get("port")));
        return synService.test(ip, port);
    }

    @GetMapping("hello")
    public String hello() {
        return "hello";
    }


}
