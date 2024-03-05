import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import { axiosConfig } from './core/services/http.service'
import store from './store'

axiosConfig();

const app = createApp(App)

app.use(router)
app.use(store)

app.mount('#app')
