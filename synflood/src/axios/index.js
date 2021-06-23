import axios from "axios";


const instance = axios.create({
    baseURL: "http://localhost:8383/",//基本路径
    timeout: 3*60*1000,// 请求超时时间
})

//请求拦截
// instance.interceptors.request.use(
//     config => {
//         //判断是否存在token，如果存在将每个页面header都添加token
//         let token = window.localStorage.getItem("token")
//         if (token !== "" && token !== null) {
//             config.headers.common['Authorization'] = token
//         }
//         return config;
//     },
//     error => Promise.reject()
// )

export default instance
