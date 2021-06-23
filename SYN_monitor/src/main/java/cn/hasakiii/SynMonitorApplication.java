package cn.hasakiii;

import org.mybatis.spring.annotation.MapperScan;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

@SpringBootApplication
@MapperScan(value = {"cn.hasakiii.mapper"})
public class SynMonitorApplication {

    public static void main(String[] args) {
        SpringApplication.run(SynMonitorApplication.class, args);
    }

}
