<template>
  <div>
    <vs-table :data="projectsList">
      <template slot="header">
        <h3>Users</h3>
      </template>
      <template slot="thead">
        <vs-th> Id</vs-th>
        <vs-th>Project Name </vs-th>
        <vs-th>User Name</vs-th>
        <vs-th>Edit</vs-th>
      </template>

      <template slot-scope="{ data }">
        <vs-tr :key="indextr" v-for="(tr, indextr) in data">
          <vs-td>
            {{ indextr + 1 }}
          </vs-td>

          <vs-td :data="data[indextr].name">
            {{ data[indextr].name }}
          </vs-td>
          <vs-td :data="data[indextr].loginName">
            {{ data[indextr].loginName }}</vs-td
          >
          <vs-td>
            <vs-button
              color="primary"
              type="border"
              icon="edit"
              @click="editProject(data[indextr])"
            ></vs-button>
            <vs-button
              color="primary"
              type="border"
              icon="delete"
              @click="deleteModal(data[indextr])"
            ></vs-button>
          </vs-td>
        </vs-tr>
      </template>
    </vs-table>
    <vs-prompt
      color="danger"
      @accept="updateProject()"
      @close="close"
      :is-valid="validName"
      :active.sync="showModal"
    >
      <div class="con-exemple-prompt">
        Enter your first and last name to <b>continue</b>.
        <vs-input
          label="Project Name"
          placeholder="Name"
          v-model="selectProject.name"
        />
        <vs-input
          label="Project Label name"
          placeholder="login"
          v-model="selectProject.loginName"
        />
        <vs-input
          label="Project password"
          placeholder="password"
          v-model="selectProject.password"
        ></vs-input>
      </div>
    </vs-prompt>
  </div>
</template>

<script>
export default {
  props: {
    projectsList: { default: [] },
  },
  data() {
    return {
      selectProject: {},
      showModal: false,
    };
  },
  methods: {
    editProject(project) {
      this.selectProject = project;
      this.showModal = true;
    },
    updateProject() {
      this.$api.put("/apimate/Project/Put", this.selectProject).then(
        (response) => {
          console.log(response);
        },
        (err) => {
          this.$store.getters.errorParse(this, err);
        }
      );
    },
    deleteModal(project) {
      this.selectProject = project;
      this.$vs.dialog({
        color: "danger",
        title: "Delete Project",
        text: "You are reali want to delete project",
        accept: this.realDelete,
      });
    },

    realDelete() {
      this.$api
        .delete("/apimate/Project/Delete?id=" + this.selectProject.id)
        .then(
          (response) => {
            console.log(response);
            if (response.isSuccess) {
              this.$store.getters.deleteById(
                this.projectsList,
                this.selectProject.id
              );
            }
          },
          (err) => {
            this.$store.getters.errorParse(this, err);
          }
        );
      console.log(project);
    },
  },
};
</script>

<style>
</style>