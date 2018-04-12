import Vue from 'vue'
import Router from 'vue-router'
import Home from '@/components/Home'
import Login_SignUp from '@/components/Login_SignUp'
import MyBoards from '@/components/MyBoards'
import MyKeeps from '@/components/MyKeeps'
import KeepZoom from '@/components/KeepZoom'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'Home',
      component: Home
    },
    {
      path: '/login',
      name: 'Login_SignUp',
      component: Login_SignUp
    },
    {
      path: '/myboards/:userId',
      name: 'MyBoards',
      props: true,
      component: MyBoards
    },
    {
      path: '/mykeeps/:userId',
      name: 'MyKeeps',
      props: true,
      component: MyKeeps
    },
    {
      path: '/keep/:keepId',
      name: 'KeepZoom',
      props: true,
      component: KeepZoom
    }
  ]
})
