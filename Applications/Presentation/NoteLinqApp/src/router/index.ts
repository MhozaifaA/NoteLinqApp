import { createRouter, createWebHistory } from 'vue-router'
import LoginView from '../views/security/LoginView.vue'
import NoteView from '../views/NoteView.vue'
import ClassificationView from '../views/ClassificationView.vue'
import Layout from '../views/shared/layouts/Layout.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
        component: Layout,
        children: [
            { path: '', component: NoteView },
            { path: 'notes', component: NoteView },
            { path: 'settings', component: ClassificationView },
        ]
    },
    {
        path: '/login',
        children: [
            { path: '', component: LoginView }
        ]
    },
  ]
})

export default router
