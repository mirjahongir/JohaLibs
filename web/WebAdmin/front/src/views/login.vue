<!-- <template>
  <div>
  <br/><br/>
    <vs-row>

      <vs-col  vs-type="flex" vs-justify="center" vs-align="center" vs-w="6">
        <image src="/images/reg.jpg"></image>
      </vs-col>

      <vs-col vs-type="flex" vs-align="center" vs-justify="center" vs-w="3">
        <vs-card class="px-xl-2 mx-auto" style="margin-top: 50%">
          <div>
            <form style="margin-left: 12%">
              <vs-input
                label="User Name"
                v-model="loginModel.userName"
                placeholder="write..."
                v-validate="'required'"
                name="userName"
              ></vs-input>
              <span class="text-danger text-sm">{{
                errors.first("userName")
              }}</span>

              <vs-input
                label="Email"
                v-model="loginModel.password"
                data-vv-validate-on="blur"
                placeholder="Password"
                v-validate.persist="'required|min:6|max:10'"
                name="email"
              ></vs-input>
              <span class="text-danger text-sm">{{
                errors.first("email")
              }}</span>
            </form>
            <br />
            <vs-button
              @click="loginUser()"
              color="rgb(187, 138, 200)"
              style="width: 70%; margin-left: 15%"
              >Login</vs-button
            >
          </div>
        </vs-card>
      </vs-col>
    </vs-row>
  </div>
</template>

<script>
export default {
  data() {
    return {
      loginModel: {
        userName: "",
        password: "",
      },
    };
  },
  methods: {
    loginUser() {
      this.$api.post("/apimate/Account/Login", this.loginModel).then(
        (response) => {
          console.log(response);
          this.$api.setToken(response.accessToken, response.refreshToken);
          this.$router.push({ path: "/" });
        },
        (err) => {
          this.$store.getters.errorParse(this, err);
        }
      );
    },
  },
  mounted() {},
};
</script>

<style>
</style> -->

<template>
    <div class="h-screen flex w-full bg-img vx-row no-gutter items-center justify-center" id="page-login">
        <div class="vx-col sm:w-1/2 md:w-1/2 lg:w-3/4 xl:w-3/5 sm:m-0 m-4">
            <vx-card>
                <div slot="no-body" class="full-page-bg-color">
                    <div class="vx-row no-gutter justify-center items-center">
                        <div class="vx-col hidden lg:block lg:w-1/2">
                            <img src="@/assets/images/pages/login.png" alt="login" class="mx-auto">
                        </div>
                        <div class="vx-col sm:w-full md:w-full lg:w-1/2 d-theme-dark-bg">
                            <div class="p-8">
                                <div class="vx-card__title mb-8">
                                    <h4 class="mb-4">Login</h4>
                                    <p>Welcome back, please login to your account.</p>
                                </div>
                                <vs-input
                                    v-validate="'required|email|min:3'"
                                    data-vv-validate-on="blur"
                                    name="email"
                                    icon="icon icon-user"
                                    icon-pack="feather"
                                    label-placeholder="Email"
                                    v-model="email"
                                    class="w-full no-icon-border"/>
                                <span class="text-danger text-sm">{{ errors.first('email') }}</span>

                                <vs-input
                                    data-vv-validate-on="blur"
                                    v-validate="'required|min:6|max:10'"
                                    type="password"
                                    name="password"
                                    icon="icon icon-lock"
                                    icon-pack="feather"
                                    label-placeholder="Password"
                                    v-model="password"
                                    class="w-full mt-6 no-icon-border" />
                                <span class="text-danger text-sm">{{ errors.first('password') }}</span>

                                <div class="flex flex-wrap justify-between my-5">
                                    <vs-checkbox v-model="checkbox_remember_me" class="mb-3">Remember Me</vs-checkbox>
                                    <router-link to="/pages/forgot-password">Forgot Password?</router-link>
                                </div>
                                <vs-button  type="border" @click="registerUser">Register</vs-button>
                                <vs-button class="float-right" :disabled="!validateForm" @click="login">Login</vs-button>

                                <!-- <SocialLogin></SocialLogin> -->

                            </div>
                        </div>
                    </div>
                </div>
            </vx-card>
        </div>
    </div>
</template>

<script>

export default {
    data() {
        return {
            email: 'demo@demo.com',
            password: 'demodemo',
            checkbox_remember_me: false
        }
    },
 
    computed: {
        validateForm() {
            return !this.errors.any() && this.email != '' && this.password != '';
        },
    },
    methods: {
        login() {
            const payload = {
                checkbox_remember_me: this.checkbox_remember_me,
                userDetails: {
                    email: this.email,
                    password: this.password
                },
                notify: this.$vs.notify
            }
            this.$store.dispatch('auth/loginAttempt', payload);
        },

        loginAuth0() {
            if (this.$store.state.auth.isUserLoggedIn()) {
                this.notifyAlreadyLogedIn();
                return false
            }
            this.$auth.login({ target: this.$route.query.to });
        },

        // Google login
        loginWithGoogle() {
            this.$store.dispatch('auth/loginWithGoogle', {notify: this.$vs.notify})
        },

        // Facebook login
        loginWithFacebook() {
            this.$store.dispatch('auth/loginWithFacebook', {notify: this.$vs.notify})
        },

        // Twitter login
        loginWithTwitter() {
            this.$store.dispatch('auth/loginWithTwitter', {notify: this.$vs.notify})
        },

        // Github login
        loginWithGithub() {
            this.$store.dispatch('auth/loginWithGithub', {notify: this.$vs.notify})
        },

        notifyAlreadyLogedIn() {
            this.$vs.notify({
                title: 'Login Attempt',
                text: 'You are already logged in!',
                iconPack: 'feather',
                icon: 'icon-alert-circle',
                color: 'warning'
            });
        },
        registerUser() {
            if(this.$store.state.auth.isUserLoggedIn()) {
                this.notifyAlreadyLogedIn();
                return false;
            }
            this.$router.push('/pages/register');
        }
    }
}
</script>


