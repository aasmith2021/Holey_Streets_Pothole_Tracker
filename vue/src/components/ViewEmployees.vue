<template>
  <div class="employee-access-table employee-access-table-top">
    <h1 class="admin-h1">All Current Employees</h1>

    <div class="scroll-section">
      <table class="table-size">
        <thead>
          <tr>
            <th class="th-style">User Id</th>
            <th class="th-style">User Name</th>
            <th class="th-style">Remove Employee Access</th>
          </tr>
        </thead>

        <tbody>
          <tr
            v-for="employee in $store.state.employees"
            v-bind:key="employee.userid"
            class="row-style"
          >
            <td>{{ employee.userId }}</td>
            <td>{{ employee.username }}</td>
            <td>
              <button @click="removeEmployeeRole(employee.userId)" class="remove">
                Remove Employee
              </button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script>
import userManagementService from "@/services/UserManagementService.js";
export default {
  name: "view-employees",
  methods: {
    removeEmployeeRole(userId) {
      userManagementService
        .removeEmployeeRole(userId)
        .then((response) => {
          if (response.status === 204) {
            this.$store.commit("DELETE_EMPLOYEE", userId);
            this.$parent.getAllEmployees();
            this.$parent.getAllNonEmployeeUsers();
          }
        })
        .catch((error) => {
          this.$parent.handleError(error);
        });
    },
  },
};
</script>

<style>
.remove {
  border-radius: 5px;
  background-color: rgb(248, 86, 86);
}
</style>