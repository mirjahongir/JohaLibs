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
        <vs-input placeholder="Name" v-model="project.name" />
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