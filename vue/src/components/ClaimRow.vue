<template>
  <tr>
    <td>{{ claim.claimId }}</td>
    <td>{{ claim.userId }}</td>
    <td>{{ claim.firstName }}</td>
    <td>{{ claim.lastName }}</td>
    <td>{{ claim.email }}</td>
    <td>{{ claim.phoneNumber }}</td>
    <td>{{ formatDate(claim.dateOfClaim) }}</td>
    <td>{{ formatDate(claim.dateOfIncident) }}</td>
    <td>{{ claim.locationOfIncidentCity }}</td>
    <td>{{ claim.locationOfIncidentState }}</td>
    <td><img v-bind:src="claim.imageLink" class="image-link" /></td>
    <td>{{ claim.descriptionOfDamage }}</td>
    <td v-if="!isEditClicked">
      {{ claim.claimStatus }}
      <div class="text-button-spacer"></div>
      <button v-if="!isEditClicked" v-on:click="changeIsEditClicked">
        Edit
      </button>
    </td>
    <td v-if="isEditClicked">
      <select
        id="claimStatus"
        name="claimStatus"
        v-model="newClaim.claimStatus"
      >
        <option>Pending</option>
        <option>Approved</option>
        <option>Denied</option>
      </select>
      <div class="text-button-spacer"></div>
      <button v-on:click="updateClaim(claim)" v-if="isEditClicked">
        Submit
      </button>
    </td>
  </tr>
</template>

<script>
import claimFormService from "../services/ClaimFormService.js";

export default {
  props: ["claim"],
  data() {
    return {
      isEditClicked: false,
      currentClaim: this.claim,
      newClaim: {},
    };
  },
  methods: {
    changeIsEditClicked() {
      Object.assign(this.newClaim, this.claim);
      this.isEditClicked = !this.isEditClicked;
    },
    formatDate(date) {
      if (date != null) {
        const month = date.substring(5, 7);
        const day = date.substring(8, 10);
        const year = date.substring(0, 4);

        date = month + "-" + day + "-" + year;
      }

      return date;
    },
    updateClaim() {
      claimFormService
        .updateClaimStatus(this.newClaim)
        .then((r) => {
          if (r.status === 200) {
            this.currentClaim = this.newClaim;
            this.$store.commit("UPDATE_CLAIM", this.newClaim);
            alert("Status Successfully Updated");
            this.isEditClicked = !this.isEditClicked;

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
          }
        })
        .catch((error) => {
          console.log(error.response.status);
        });
    },
  },
};
</script>

<style scoped>
.image-link {
  width: auto;
  height: 75px;
}

button {
  background-color: #adc178;
  border: 1px solid black;
  border-radius: 5px;
}

.text-button-spacer {
  height: 5px;
}
</style>