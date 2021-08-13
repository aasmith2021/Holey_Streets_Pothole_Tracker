<template>
  <div>
    <a
      href
      v-b-modal.modal= "'modal-' + pothole.id"
      id="button"
      class="edit-delete"
      v-on:click.prevent

    >
      <img src="/trash.ico" class="ico" />
      Delete</a
    >
    <b-modal
      :id= "'modal-' + pothole.id"
      title="Delete Pothole"
      @ok="deletePothole(pothole.id)"
      hide-header-close
      cancel-variant="danger"
      ok-variant="success"
    >
      <p class="my-4">
        Are you sure? This will permanently delete the selected pothole.
      </p>
    </b-modal>
  </div>
</template>

<script>
import potholeService from "../services/PotholeService.js";

export default {
  name: "delete-confirmation",
  props: ["pothole"],
  methods: {
    deletePothole(id) {
      potholeService
        .deletePothole(id)
        .then(() => {
          this.$store.commit("DELETE_POTHOLE", id);
        })
        .catch((e) => {
          console.log(e.status);
        });
    },
  },
};
</script>

<style scoped>
#button:hover {
    color: red;
}
</style>