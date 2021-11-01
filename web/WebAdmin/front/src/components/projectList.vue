<template>
  <vs-table :data="projectsList">
    <template slot="header">
      <h3>Users</h3>
    </template>
    <template slot="thead">
      <vs-th> Email </vs-th>
      <vs-th> Name </vs-th>
      <vs-th> Website </vs-th>
      <vs-th> Nro </vs-th>
    </template>

    <template slot-scope="{ data }">
      <vs-tr :key="indextr" v-for="(tr, indextr) in data">
        <vs-td :data="data[indextr].email">
          {{ data[indextr].email }}
        </vs-td>

        <vs-td :data="data[indextr].username">
          {{ data[indextr].name }}
        </vs-td>

        <vs-td :data="data[indextr].id">
          {{ data[indextr].id }}
        </vs-td>

        <vs-td :data="data[indextr].id">
          {{ data[indextr].website }}
        </vs-td>
      </vs-tr>
    </template>
  </vs-table>
</template>

<script>
export default {
    props:{
        projectsList
    },
    data(){
        return{

        }
    },
    methods:{

    }
};
</script>

<style>
</style>