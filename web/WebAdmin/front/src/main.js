import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import Vuesax from "vuesax"
import VeeValidate from 'vee-validate';


import 'vuesax/dist/vuesax.css'
import 'material-icons/iconfont/material-icons.css';

Vue.config.productionTip = false

Vue.use(Vuesax)
Vue.use(VeeValidate);

new Vue({
  router,
  store,
  render: h => h(App),
  created(){
    this.$api.checkToken();
  }
}).$mount('#app')