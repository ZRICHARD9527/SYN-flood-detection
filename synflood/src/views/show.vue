<template>
    <el-tabs type="border-card">
        <el-tab-pane>
            <span slot="label"><i class="el-icon-date"></i> SYN洪泛攻击检测</span>

            <el-row>
                <el-col :span="3">
                    <span style="font-size: 10px">监听ip: </span>
                    <el-input type="text" v-model="listenid"
                              style="width: 120px;height: 25px"
                              size="medium"
                              maxlength="15"
                              minlength="10"
                    ></el-input>
                </el-col>
                <el-col :span="3">
                    <span style="font-size: 10px">监听端口: </span>
                    <el-input type="text" v-model="listenport"
                              style="width: 120px;height: 25px"
                              size="medium"
                              maxlength="10"
                              minlength="1"
                    ></el-input>
                </el-col>
                <el-col :span="2">
                    <el-button type="success" plain @click="getData() " size="medium">开始检测</el-button>
                </el-col>
            </el-row>

            <el-form label-position="left" label-width="80px" style="margin-top: 50px">
                <el-form-item label="情况">
                    <el-input v-model="rData.ifAttacked" :readonly="true" ></el-input>
                </el-form-item>
                <el-form-item label="信息">
                    <el-input v-model="rData.msg" :readonly="true"></el-input>
                </el-form-item>
                <el-form-item label="攻击次数">
                    <el-input v-model="rData.invalidSYN" :readonly="true"></el-input>
                </el-form-item>
                <el-form-item label="疑似IP">
                    <el-input v-model="rData.attackIP" :readonly="true"></el-input>
                </el-form-item>
            </el-form>


        </el-tab-pane>
    </el-tabs>
</template>

<script>
    export default {
        name: "show",
        data() {
            return {
                listenid: null,
                listenport: -1,//-1为监听所有端口
                rData: {
                    ifAttacked: null,
                    invalidSYN: null,
                    msg: null,
                    attackIP: null
                }

            }
        },
        created() {
            this.$http.get("/hello")
                .then(res => {
                    console.log("res\n")
                    console.log(res)
                })
            //this.getData()
        },
        watch: {
            rData() {
                this.timer()
            }
        },
        destroyed() {
            clearTimeout(this.timer)
        },
        methods: {
            //获取数据
            getData() {
                this.rData = {
                    ifAttacked: "正常",
                    invalidSYN: 0,
                    attackIP: "",
                    msg: "暂无返回信息"
                }
                const _this = this
                this.$http.post('/test', {
                    "ip": _this.listenid,
                    "port": _this.listenport
                }).then(res => {
                    console.log(res)
                    if (res.data.data.ifAttacked === 0) {
                        _this.rData.ifAttacked = "正常"
                        _this.rData.invalidSYN = 0
                        _this.rData.msg = res.data.msg
                        _this.rData.attackIP = ""
                    } else {
                        _this.rData.ifAttacked = "正在受到攻击"
                        _this.rData.invalidSYN = res.data.data.invalidSYN
                        _this.rData.msg = res.data.msg
                        _this.rData.attackIP = res.data.data.attackIP
                    }
                }).catch((error) => {
                        console.log(error)
                    })
            },
            //定时器
            timer() {
                return setTimeout(() => {
                    this.getData()
                }, 10 * 1000)

            },
        }
    }
</script>

<style scoped>

</style>