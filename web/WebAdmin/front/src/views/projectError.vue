<template>
  <div>
    <vs-row>
      <vs-col></vs-col>
      <vs-col>
        <add-projec-error-modal
          :projectId.sync="query.id"
          :add-error="addError"
          :selectProject.sync="selectProject"
        ></add-projec-error-modal>
      </vs-col>
    </vs-row>
    <vs-row>
      <vs-col></vs-col>
      <vs-col>
        <vs-table :data="errorList">
          <template slot="header">
            <h3>Project Error list</h3>
          </template>

          <template slot="thead">
            <vs-th>http Status</vs-th>
            <vs-th>Code</vs-th>
            <vs-th>Message</vs-th>
          </template>

          <template slot-scope="{ data }">
            <vs-tr :key="index" v-for="(tr, index) in data">
              <vs-td :data="data[index].message">
                {{ data[index].errorModel.httpStatus }}</vs-td
              >
              <vs-td> {{ data[index].errorModel.code }}</vs-td>
              <vs-td>{{ data[index].errorModel.message }}</vs-td>
              <vs-td><vs-button @click="changeError(tr)"></vs-button></vs-td>
            </vs-tr>
          </template>
        </vs-table>
      </vs-col>
    </vs-row>
  </div>
</template>

<script>
import addProjecErrorModal from "../components/addProjecErrorModal.vue";

export default {
  components: { addProjecErrorModal },
  data() {
    return {
      query: {
        id: "",
        name: "",
        pageNumber: 0,
        pageSize: 100,
      },
      errorList: [],
      selectProject: {},
    };
  },
  methods: {
    changeError(model) {
      console.log(model);
      this.selectProject = model;
    },
    addError(newError) {
      this.errorList.push(newError);
    },
    getProjectError() {
      let query = new URLSearchParams(this.query);

      this.$api.get("/apimate/ProjectError/Get?" + query).then(
        (response) => {
          console.log(response);
          this.errorList = response.result;
          console.log(this.errorList);
        },
        (err) => {
          this.$store.getters.errorParse(this, err);
        }
      );
    },
  },
  created() {
    this.query.id = this.$route.params.id;
    this.getProjectError();
  },
};
</script>

<style>
</style>