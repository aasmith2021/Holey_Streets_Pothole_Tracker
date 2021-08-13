<template>
  <div class="employee-access-table employee-access-table-top">
    <h1 class="admin-h1">Users Requesting Employee Access</h1>

    <div class="scroll-section">
      <table class="table-size">
        <thead>
          <tr>
            <th class="th-style">User Id</th>
            <th class="th-style">User Name</th>
            <th class="th-style">Approve</th>
            <th class="th-style">Deny</th>
          </tr>
        </thead>

        <tbody>
          <tr
            v-for="request in this.$store.state.requests"
            v-bind:key="request.userid"
            class="row-style"
          >
            <td>{{ request.userId }}</td>
            <td>{{ request.username }}</td>
            <td>
              <button @click="approveRequest(request.userId)" class="approve">Approve</button>
            </td>
            <td>
              <button @click="denyRequest(request.userId)" class="deny">Deny</button>
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
  name: "active-requests",
  methods: {
    approveRequest(userId) {
      userManagementService
        .approveRequest(userId)
        .then((response) => {
          if (response.status === 200) {
            this.$store.commit("DELETE_REQUEST",userId);
            this.$parent.getAllActiveRequests();
            this.$parent.getAllEmployees();
            this.$parent.getAllNonEmployeeUsers();
          }
        })
        .catch((error) => {
          this.$parent.handleError(error);
        });
    },
    denyRequest(userId) {
      userManagementService
        .denyRequest(userId)
        .then((response) => {
          if (response.status === 200) {
            this.$store.commit("DELETE_REQUEST",userId);
            this.$parent.getAllActiveRequests();
            this.$parent.getAllNonEmployeeUsers();
          }
        })
        .catch((error) => {
          this.$parent.handleError(error);
        });
    },
  }
};
</script>

<style>
@media only screen and (min-width: 375px) {
  td {
    padding: 5px 10px;
  }
  button {
    margin-right: 5px;
  }
}

@media only screen and (max-width: 375px) {
  td {
    padding: 3px 0;
  }
  button {
    margin-right: 0;
    border-radius: 5px;
  }
}

th {
  text-transform: uppercase;
  padding: 10px;
  margin-top: 10px;
}
.deny {
  background-color: rgb(248, 86, 86);
  border-radius: 5px;
}
.approve {
  background-color: #adc178;
  border-radius: 5px;
}
</style>
