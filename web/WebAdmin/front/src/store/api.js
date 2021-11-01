export default {
    install(Vue, options) {
      Vue.prototype.$api = {
        axios() {
          return options.http
        },
        send(requestType, apiRoute, params = {}, data = null) {
          return new Promise((resolve, reject) => {
  
            options.http[requestType](apiRoute, params || [], data)
              .then(res => {
                resolve(res.data)
              })
              .catch(err => {
                // this.errorHandler(err)
                reject(err)
              })
          })
        },
        get(apiRoute, params = null) {
          return this.send('get', apiRoute, params)
        },
        post(apiRoute, data = null) {
          return this.send('post', apiRoute, data)
        },
        put(apiRoute, data = null) {
          return this.send('put', apiRoute, data)
        },
        patch(apiRoute, data = null) {
          return this.send('patch', apiRoute, data)
        },
        delete(apiRoute, params = null) {
          return this.send('delete', apiRoute, params)
        },
        errorHandler(err) {
  
        },
        
        setToken(modal) {
          window.localStorage.setItem("token", modal.accessToken);
          window.localStorage.setItem("refreshToken", modal.refreshToken);
          options.http.defaults.headers.common["Authorization"] = "Bearer " + modal.accessToken;
        },
  
        checkToken() {
          let token = window.localStorage.getItem("token");
          if(!token)return false;
          options.http.defaults.headers.common["Authorization"] = "Bearer " + token;
          return true;
        }
      }
    }
  }
