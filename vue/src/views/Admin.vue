<template>
  <div>
    <div class="title-and-pictures">
      <img
        class="left-pic tire-pic"
        src="../../public/tire_track_left.jpg"
        alt="Tire track picture"
      />
      <h1 class="centered-h1 title">Admin Dashboard</h1>
      <img
        class="right-pic tire-pic"
        src="../../public/tire_track_right.jpg"
        alt="Tire track picture"
      />
    </div>

    <br />

    <div class="admin-tables">
      <div class="admin-tables-top">
        <active-requests />
        <view-employees />
        <all-users />
      </div>
      <div class="admin-tables-bottom">
        <all-claims />
      </div>
    </div>
  </div>
</template>

<script>
import ActiveRequests from "../components/ActiveRequests.vue";
import ViewEmployees from "../components/ViewEmployees.vue";
import AllUsers from "../components/AllUsers.vue";
import userManagementService from "@/services/UserManagementService.js";
import AllClaims from "../components/AllClaims.vue";

export default {
  components: {
    ActiveRequests,
    ViewEmployees,
    AllUsers,
    AllClaims,
  },
  methods: {
    getAllEmployees() {
      userManagementService
        .getListOfEmployees()
        .then((response) => {
          if (response.status === 200) {
            this.$store.commit("SET_EMPLOYEES_LIST", response.data);
          }
        })
        .catch((error) => {
          this.handleError(error);
        });
    },
    getAllActiveRequests() {
      userManagementService
        .getListOfRequests()
        .then((response) => {
          if (response.status === 200) {
            this.$store.commit("SET_REQUESTS_LIST", response.data);
          }
        })
        .catch((error) => {
          this.handleError(error);
        });
    },
    getAllNonEmployeeUsers() {
      userManagementService
        .getAllNonEmployeeUsers()
        .then((response) => {
          if (response.status === 200) {
            this.$store.commit("SET_NON_EMPLOYEE_USERS_LIST", response.data);
          }
        })
        .catch((error) => {
          this.handleError(error);
        });
    },
    handleError(error) {
      console.log(error);
    },
  },
  created() {
    this.getAllEmployees();
    this.getAllActiveRequests();
    this.getAllNonEmployeeUsers();
  },
};
</script>

<style>
@media only screen and (min-width: 1440px) {
  .admin-tables {
    display: flex;
    flex-direction: column;
    align-items: center;
    gap: 5vh;
    margin: 0 5vh 5vh 5vh;
  }

  .admin-tables-top {
    height: 35vh;
    gap: 3vh;
    min-height: 250px;
    width: 100%;
    display: flex;
    flex-direction: row;
    align-items: flex-start;
    justify-content: space-between;
  }

  .admin-tables-bottom {
    height: 50vh;
    min-height: 250px;
    width: 100%;
    display: flex;
    flex-direction: row;
    align-items: stretch;
    justify-content: stretch;
  }

  .scroll-section {
    height: 27vh;
    overflow: auto;
  }

  .scroll-section-all-users {
    height: 22vh;
    overflow: auto;
  }
}

@media only screen and (max-width: 1440px) {
  .admin-tables {
    display: flex;
    flex-direction: column;
    align-items: center;
    gap: 5vh;
    margin: 0 5vh 5vh 5vh;
  }

  .admin-tables-top {
    height: 35vh;
    gap: 3vh;
    min-height: 250px;
    width: 100%;
    display: flex;
    flex-direction: row;
    align-items: flex-start;
    justify-content: space-between;
  }

  .admin-tables-bottom {
    height: 50vh;
    min-height: 250px;
    width: 100%;
    display: flex;
    flex-direction: row;
    align-items: stretch;
    justify-content: stretch;
  }

  .scroll-section {
    height: 27vh;
    overflow: auto;
  }

  .scroll-section-all-users {
    height: 20vh;
    overflow: auto;
  }
}

@media only screen and (max-width: 1220px) {
  .admin-tables {
    display: flex;
    flex-direction: column;
    align-items: center;
    gap: 5vh;
    margin: 0 5vh 5vh 5vh;
  }

  .admin-tables-top {
    height: 35vh;
    gap: 3vh;
    min-height: 250px;
    width: 100%;
    display: flex;
    flex-direction: row;
    align-items: flex-start;
    justify-content: space-between;
  }

  .admin-tables-bottom {
    height: 50vh;
    min-height: 250px;
    width: 100%;
    display: flex;
    flex-direction: row;
    align-items: stretch;
    justify-content: stretch;
  }

  .scroll-section {
    height: 27vh;
    overflow: auto;
  }

  .scroll-section-all-users {
    height: 22vh;
    overflow: auto;
  }
}

@media only screen and (max-width: 1220px) {
    .admin-tables {
    display: flex;
    flex-direction: column;
    align-items: center;
    gap: 5vh;
    margin: 0 5vh 5vh 5vh;
  }

  .admin-tables-top {
    height: 35vh;
    gap: 3vh;
    min-height: 250px;
    width: 100%;
    display: flex;
    flex-direction: row;
    align-items: flex-start;
    justify-content: space-between;
  }

  .admin-tables-bottom {
    height: 50vh;
    min-height: 250px;
    width: 100%;
    display: flex;
    flex-direction: row;
    align-items: stretch;
    justify-content: stretch;
  }

  .scroll-section {
    height: 27vh;
    overflow: auto;
  }

  .scroll-section-all-users {
    height: 19vh;
    overflow: auto;
  }
}

@media only screen and (max-width: 1060px) {
  .admin-tables {
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    gap: 3vh;
  }

  .admin-tables-top {
    height: 120vh;
    width: 100%;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
  }

  div.employee-access-table-top {
  height: 50vh;
  min-height: 250px;
  width: 100%;
  }

  .scroll-section {
    height: 28vh;
    overflow: auto;
  }

  .scroll-section-all-users {
    height: 24vh;
    overflow: auto;
  }
}

@media only screen and (max-width: 450px) {
  .admin-tables {
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    gap: 3vh;
  }

  .admin-tables-top {
    height: 120vh;
    width: 100%;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
  }

  div.employee-access-table-top {
  height: 50vh;
  min-height: 250px;
  width: 100%;
  }

  .scroll-section {
    height: 28vh;
    overflow: auto;
  }

  .scroll-section-all-users {
    height: 17vh;
    overflow: auto;
  }
}

div.employee-access-table {
  border: 1px solid black;
  border-radius: 10px;
  padding-bottom: 10px;
}

div.employee-access-table-top {
  height: 35vh;
  min-height: 250px;
  width: 100%;
}

div.employee-access-table-bottom {
  width: 100%;
  height: 50vh;
  min-height: 250px;
}

h1.admin-h1 {
  padding: 5px 10px;
  font-size: 24px;
  font-family: "Luckiest Guy", cursive;
  text-align: center;
  border-bottom: 4px solid #adc178;
}

th,
td {
  text-align: center;
}

table {
  margin: auto;
}

.table-size{
    width: 100%;
}

.row-style {
    border-top: 1px solid black;
}

.th-style{
    text-align: center;
    position: sticky;
    top: 0;
    background-color: #adc178;
}
</style>