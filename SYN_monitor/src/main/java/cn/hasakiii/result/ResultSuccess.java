package cn.hasakiii.result;

public class ResultSuccess extends ResultModel{
    public ResultSuccess(String msg, Object data) {
        super(1, msg, data);
    }
    public ResultSuccess(String msg) {
        super(1, msg, null);
    }
}
