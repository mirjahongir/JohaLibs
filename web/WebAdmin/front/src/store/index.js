import Vue from 'vue'
import Vuex from 'vuex'
import api from "./api";
import axios from "axios"
const url = "http://127.0.0.1:4050";
Vue.use(Vuex)

var http = axios.create({
  baseURL: url
});
Vue.use(api, {
  http
});

export default new Vuex.Store({
  state: {},
  mutations: {},
  actions: {},
  getters: {
    showError: (state, getters) => (_this, message, title = "") => {
      _this.
      $vs.notify({
        title: title,
        text: message,
        color: 'danger',
        position: 'top-left'
      })
    },
    erroParse:(state, getters)=>(_this, error)=>{
      console.log(error);

    }
  },
  modules: {}
})