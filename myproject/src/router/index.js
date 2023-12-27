import { createRouter, createWebHistory } from 'vue-router'

const routes = [
  {
    path: '/',
    name: 'home',
    component:()=>import('../views/Custom/index')
  },
  {
    path: '/custom/index',
    name: 'Index',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/Custom/index.vue')
  },
  {
    path:'/about',
    component:()=>import('../views/AboutView.vue')
  },
  {
    path:'/orderdetail',
    component:() => import('../views/Admin/Orderdetail.vue')
  },
  {
    path:'/cart',
    component:()=>import('../views/Custom/CartView.vue')
  },
  {
    path:'/admin',
    component:()=>import('../views/Admin/HomeAdmin.vue'),
    // meta: { requiresAuth: true }
  },
  {
    path:'/products',
    component:()=>import('../views/Admin/Products.vue')
  },
  {
    path:'/order',
    component:()=>import('../views/Admin/Order.vue')
  },
  {
    path: '/users',
    component:()=> import('../views/Admin/Users.vue')
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
});
router.beforeEach((to, from, next) => {
  // Kiểm tra nếu route yêu cầu xác thực và người dùng chưa đăng nhập
  if (to.meta.requiresAuth && !store.getters.isLogin) {
    // Chuyển hướng đến trang đăng nhập
    next('/login');
  } else {
    // Cho phép truy cập
    next();
  }
});


export default router
