import Vue from 'vue'
import App from './App.vue'
import router from './router'

Vue.config.productionTip = false
Vue.prototype.$http = "http://localhost:50598/api";

new Vue({
  router,
  render: h => h(App)
}).$mount('#app')
