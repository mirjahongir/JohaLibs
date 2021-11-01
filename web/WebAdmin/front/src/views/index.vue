<template>
  <div>
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
export default {
  components: {
    ProjectList,
  },
  data() {
    return {
      propjects:[]
    };
  },
  methods: {},
  mounted() {
      this.$api.get("/").then(response=>{});
  },
};
</script>

<style>
</style>