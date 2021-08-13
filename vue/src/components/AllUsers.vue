<template>
  <div class="employee-access-table employee-access-table-top">
    <h1 class="admin-h1">All Users Without Employee Access</h1>
    <h2 class="all-non-employee-users-subtitle"><em>Users without employee access who do not have an active request to become an employee</em></h2>
    
    <div class="scroll-section-all-users">
      <table class="table-size">
        <thead>
          <tr>
            <th class="th-style">User Id</th>
            <th class="th-style">User Name</th>
            <th class="th-style">Grant Employee Access</th>
          </tr>
        </thead>

        <tbody>
          <tr
            v-for="user in allNonEmployeeUsersWithoutActiveRequests"
            v-bind:key="user.userid"
            class="row-style"
          >
            <td>{{ user.userId }}</td>
            <td>{{ user.username }}</td>
            <td>
              <button @click="giveUserEmployeeRole(user.userId)" class="give-access">
                Give Employee Access
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
  name: "all-users",
  methods: {
    giveUserEmployeeRole(userId) {
      userManagementService
        .approveRequest(userId)
        .then((response) => {
          if (response.status === 200) {
            this.$parent.getAllEmployees();
            this.$parent.getAllNonEmployeeUsers();
          }
        })
        .catch((error) => {
          this.$parent.handleError(error);
        });
    }
  },
  computed: {
    allNonEmployeeUsersWithoutActiveRequests() {
      const allActiveRequestUserIds = [];
      this.$store.state.requests.forEach(element => {
        allActiveRequestUserIds.push(element.userId);
      });

      const nonEmployeeUsersWithoutActiveRequests = this.$store.state.nonEmployeeUsers.filter(user => {
        return !allActiveRequestUserIds.includes(user.userId);
      });

      return nonEmployeeUsersWithoutActiveRequests;
    }
  }
};
</script>

<style >
.give-access {
  background-color: #adc178;
  border-radius: 5px;
}

h2.all-non-employee-users-subtitle {
  font-size: 14px;
  color: darkgrey;
  padding: 0 10px;
}
</style>