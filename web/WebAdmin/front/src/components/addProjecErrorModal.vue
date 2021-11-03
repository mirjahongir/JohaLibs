<template>
  <div>
    <vs-button @click="showPromp"> Add Error </vs-button>
    <vs-prompt
      color="danger"
      @cancel="cancelModal"
      @accept="addProjectError()"
      @close="close"
      :is-valid="validName"
      :active.sync="showModal"
    >
      <div>
        <vs-input
          label="Message"
          v-model="newModal.errorModel.message"
        ></vs-input>
        <vs-input
          label="HttpStatus"
          v-model="newModal.errorModel.httpStatus"
        ></vs-input>
        <vs-input label="Code" v-model="newModal.errorModel.code"></vs-input>
        <vs-input
          label="Uzb Text"
          v-model="newModal.errorModel.uzbText"
        ></vs-input>
        <vs-input
          label="Rus Text"
          v-model="newModal.errorModel.rusText"
        ></vs-input>
        <vs-input label="Eng Text" v-model="newModal.errorModel.engText">
        </vs-input>
      </div>
    </vs-prompt>
  </div>
</template>

<script>
export default {
  data() {
    return {
      showModal: false,
      isUpdate: false,
      newModal: {
        projectId: "",
        errorModel: {
          code: 0,
          httpStatus: 400,
          message: "",
          link: "",
          uzbText: "",
          rusText: "",
          engText: "",
        },
      },
    };
  },
  props: {
    projectId: { default: "" },
    selectProject: { default: null },
  },
  methods: {
    cancelModal() {},
    addProjectError() {
      this.newModal.errorModel.code = parseInt(this.newModal.errorModel.code);
      this.newModal.errorModel.httpStatus = parseInt(
        this.newModal.errorModel.httpStatus
      );
      if (this.isUpdate) {
        this.updateProjectError();
        return;
      }
      this.$api.post("/apimate/ProjectError/Post", this.newModal).then(
        (response) => {
          console.log(response);
          this.$emit("addError", response.result.errorModel);
        },
        (err) => {
          this.$store.getters.errorParse(this, err);
        }
      );
    },
    updateProjectError() {
      this.$api.put("/apimate/ProjectError/Put", this.newModal).then(
        (response) => {
          console.log(response);
          this.isUpdate = false;
        },
        (err) => {
          this.$store.getters.errorParse(this, err);
        }
      );
    },
    showPromp() {
      this.newModal.projectId = this.projectId;
      if (!this.newModal.projectId) {
        console.log("projectId not fount");
        return;
      }
      this.showModal = true;
    },
  },
  mounted() {},
  watch: {
    selectProject: function (val) {
      console.log(val);
      this.newModal = val;
      this.isUpdate = true;
      this.showModal= true;
    },
  },
};
</script>

<style>
</style>