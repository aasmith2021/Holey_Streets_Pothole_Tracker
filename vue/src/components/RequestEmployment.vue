<template>
  <div>
    <a href v-b-modal.modal-1 id="button">Apply to Holey Streets</a>
    <b-modal
      id="modal-1"
      title="Apply to Holey Streets"
      @ok="requestEmployment"
      hide-header-close
      cancel-variant="danger"
      ok-variant="success"
    >
      <p class="my-4">
        Holey Streets expects the best from its volunteers! If you would like to
        dedicate your personal time to eliminating these pesky tire traps, click
        OK below! Please allow 7-10 business days for your background check to
        process...
      </p>
    </b-modal>
  </div>
</template>

<script>
import userManagementService from "@/services/UserManagementService.js";

export default {
  methods: {
    requestEmployment() {
      userManagementService
        .requestEmployment()
        .then((response) => {
          if (response.status === 200) {
            alert("Submission Complete");
          }
        })
        .catch((response) => {
          if (response.response.status === 409){
            alert("Could not complete because you already have a pending request. Please wait for your request to be processed.");
          }
         console.log(response);
        });
    },
  },
};
</script>

<style>
#button {
  text-decoration: none;
  color: black;
  justify-content: flex-end;
  margin-right: 20px;
  border-radius: 5px;
}

</style>