import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../components/HelloWorld.vue'
import EmployeeList from '../view/employees/EmployeesList.vue'

Vue.use(VueRouter)

const routers = [
    {path: '/', name : 'Home', component : Home},
    {path: '/employee', name : 'Employee', component : EmployeeList}
]

const router = new VueRouter({
    mode : 'history',
    routes : routers,
    linkActiveClass: 'navbar__item-focus'
})

export default router