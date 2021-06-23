import Vue from 'vue'
import VueRouter from 'vue-router'
Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'SYN洪泛攻击检测',
    meta:{
      title:"SYN洪泛攻击检测"
    },
    component: () => import( '../views/show.vue')
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
