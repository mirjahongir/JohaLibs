import Vue from 'vue'
import VueRouter from 'vue-router'

Vue.use(VueRouter)

const routes = [{
    path: "/",
    component: () => import("../views/index.vue")
  },
  {
    path: "/login",
    component: () => import("../views/login.vue")
  },
  {
    path: "/register",
    component: () => import("../views/register")
  },
  {
    path: "/projectError/:id",
    component: () => import("../views/projectError")
  }
]

const router = new VueRouter({
  routes
})

export default router