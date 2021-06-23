import Vue from 'vue'
import './plugins/axios'
import App from './App.vue'
import Notifications from 'vue-notification'
import notificationHub from './notification-hub'
import './styles/app.css';

Vue.config.productionTip = false

Vue.use(Notifications)
Vue.use(notificationHub)

new Vue({
  render: h => h(App),
}).$mount('#app')