<template>
  <div>
    <vs-row>
      <vs-col vs-w="9">
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
</style>