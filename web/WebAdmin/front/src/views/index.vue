<template>
  <div>
    <vs-row>
      <vs-col></vs-col>
      <vs-col>
        <add-project-modal></add-project-modal>
      </vs-col>
    </vs-row>
    <vs-row>
      <vs-col
        vs-type="flex"
        vs-justify="flex-start"
        vs-align="flex-start"
        vs-offset="2"
      >
        Project List
        <br />
        <project-list :projectsList.sync="propjects"></project-list>
      </vs-col>
    </vs-row>
  </div>
</template>

<script>
import ProjectList from "../components/projectList.vue";

import AddProjectModal from "../components/addProjectModal.vue";
export default {
  components: {
    ProjectList,
    AddProjectModal,
  },
  data() {
    return {
      propjects: [],
      query: {
        id: "",
        name: "",
        pageNumber: 0,
        pageSize: 100,
      },
    };
  },
  methods: {
    getQuery() {
      let query = new URLSearchParams(this.query);
      this.$api.get("/apimate/Project/Get?" + query).then(
        (response) => {
          console.log(response);
        },
        (err) => {
          this.$store.getters.errorParse(this, err);
        }
      );
    },
  },
  mounted() {
    this.getQuery();
  },
};
</script>

<style>
</style>