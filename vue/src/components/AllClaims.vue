<template>
    <div class="employee-access-table employee-access-table-bottom">
      <h1 class="admin-h1">Damage Claims</h1>
      <div class="scroll-section">
        <table class="table-size">
          <thead>
            <tr>
              <th class="th-style">Claim Id</th>
              <th class="th-style">User Id</th>
              <th class="th-style">First Name</th>
              <th class="th-style">Last Name</th>
              <th class="th-style">Email</th>
              <th class="th-style">Phone Number</th>
              <th class="th-style">Date of Claim</th>
              <th class="th-style">Date of Damage</th>
              <th class="th-style">City</th>
              <th class="th-style">State</th>
              <th class="th-style image-link">Image</th>
              <th class="th-style">Description of Damage</th>
              <th class="th-style">Claim Status</th>
            </tr>
          </thead>

          <tbody>
            <claim-row
            v-for="claim in this.$store.state.claims"
            v-bind:claim="claim"
            v-bind:key="claim.claimId"
            class="row-style"
            />
          </tbody>
        </table>
      </div>
    </div>
</template>

<script>
import claimFormService from "../services/ClaimFormService.js";
import ClaimRow from "./ClaimRow.vue";

export default {
  components: {
    ClaimRow
  },
  created() {
      claimFormService
      .list()
      .then((response) => {
          if (response.status === 200) {
          this.$store.commit("SET_CLAIMS_LIST", response.data);
          }
      })
      .catch((error) => {
          console.log(error.response.status);
      });
  },
};
</script>

<style scoped>
.scroll-section {
  height: 87%;
  overflow: auto;
}

thead > tr > th.th-style {
  font-size: 14px;
  padding: 3px;
  position: sticky;
  top: 0;
  background-color: #adc178;
}
</style>