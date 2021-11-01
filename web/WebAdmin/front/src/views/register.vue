<template>
  <div>
    <vs-row>
      <vs-col vs-type="flex" vs-justify="center" vs-align="center" vs-w="3">
      </vs-col>

      <vs-col vs-type="flex" vs-justify="center" vs-align="center" vs-w="6">
        <vs-card>
          <div>
            <vs-input
              label="User Name"
              v-model="registerModel.userName"
              placeholder="write..."
              v-validate="'required'"
              name="userName"
            ></vs-input>
            <span class="text-danger text-sm">{{
              errors.first("userName")
            }}</span>

            <vs-input
              label="Email"
              v-model="registerModel.email"
              data-vv-validate-on="blur"
              placeholder="Email"
              v-validate.persist="'required|email'"
              name="email"
            ></vs-input>
            <span class="text-danger text-sm">{{ errors.first("email") }}</span>

            <vs-input
              label="Password"
              v-model="registerModel.password"
              placeholder="Password"
              v-validate="'required|min:6|max:10'"
              name="password"
            ></vs-input>
            <span class="text-danger text-sm">
              {{ errors.first("password") }}
            </span>

            <vs-input
              label="Comfirm Password"
              v-model="registerModel.confirmPassword"
              placeholder="Compre Password"
              v-validate="'required|min:6|max:10'"
              name="confirmPassword"
            ></vs-input>
            <span class="text-danger text-sm">
              {{ errors.first("confirmPassword") }}
            </span>
          </div>
          <div slot="footer">
            <vs-row vs-justify="flex-end">
              <vs-button
                type="gradient"
                color="danger"
                icon="cancel"
              ></vs-button>
              <vs-button
                color="primary"
                icon="done"
                @click="checkValidate()"
              ></vs-button>
            </vs-row>
          </div>
        </vs-card>
      </vs-col>

      <vs-col vs-type="flex" vs-justify="center" vs-align="center" vs-w="3">
      </vs-col>
    </vs-row>
  </div>
</template>

<script>
export default {
  data() {
    return {
      registerModel: {
        userName: "",
        password: "",
        confirmPassword: "",
        email: "",
      },
    };
  },
  methods: {
    checkValidate() {
      this.$validator.validateAll().then((result) => {
        if (result) {
          this.registerUser();
        }
      });
    },
    registerUser() {
      if (this.registerModel.password != this.registerModel.confirmPassword) {
        this.$store.getters.showError(this, "Confirm Password");
      }
      this.$api.post("/apimate/Project/Post", this.registerModel).then(
        (response) => {
          if (response.success) {
            this.$router.push({ path: "/login" });
          } else {
            this.$store.getters.showError(
              this,
              "Register error ",
              "user egsist"
            );
          }
        },
        (err) => {
          this.$store.getters.erroParse(this, err);
        }
      );
    },
  },
  mounted() {},
};
</script>

<style>
</style>