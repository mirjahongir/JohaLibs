<template>
  <div>
    <vs-button @click="showPromp">Add Project</vs-button>
    <vs-prompt
      color="danger"
      @cancel="(valMultipe.value1 = ''), (valMultipe.value2 = '')"
      @accept="addProject()"
      @close="close"
      :is-valid="validName"
      :active.sync="showModal"
    >
      <div class="con-exemple-prompt">
        Enter your first and last name to <b>continue</b>.
        <vs-input
          label="Project Name"
          placeholder="Name"
          v-model="project.name"
        />
        <vs-input
          label="Project Label name"
          placeholder="login"
          v-model="project.LoginName"
        />
        <vs-input
          label="Project password"
          placeholder="password"
          v-model="project.password"
        ></vs-input>
      </div>
    </vs-prompt>
  </div>
</template>

<script>
export default {
  data() {
    return {
      showModal: false,
      project: {
        name: "",
        loginName: "",
        password: "",
      },
    };
  },
  methods: {
    showPromp() {
      this.showModal = true;
    },
    addProject() {
      console.log(this.project);
      this.$api.post("/apimate/project/Post", this.project).then(
        (response) => {
          console.log(response);
          
          this.$emit("addProject", response.project)
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