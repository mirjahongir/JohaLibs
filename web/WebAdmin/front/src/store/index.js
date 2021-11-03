import Vue from 'vue'
import Vuex from 'vuex'
import api from "./api";
import axios from "axios"
const url = "http://127.0.0.1:2030";
Vue.use(Vuex)

var http = axios.create({
  baseURL: url
});
Vue.use(api, {
  http
});

export default new Vuex.Store({
  state: {
    selectProject: null,
  },
  mutations: {},
  actions: {},
  getters: {
    deleteById: (state, getters) => (list, id) => {
      for (let i = 0; i < list.length; i++) {
        if (list[i].id == id) {
          list.splice(i, 1);
          break;
        }

      }

    },
    showError: (state, getters) => (_this, message, title = "") => {
      _this.
      $vs.notify({
        title: title,
        text: message,
        color: 'danger',
        position: 'top-left'
      })
    },
    errorParse: (state, getters) => (_this, error) => {
      switch (error.response.status) {
        case 401:
          _this.$router.push({
            path: "/login"
          });
          break;
        case 400:
          break;
        case 500:
          break;
        default:
          break;
      }
      console.log(error.response.status);


    }
  },
  modules: {}
})