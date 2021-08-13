<template>
  <div class="list-of-potholes">
    <div class="subtitle-box">
      <h2 class="subtitle">Potholes You Have Reported</h2>
    </div>
    <b-table
      :items="potholes"
      :fields="visibleFields"
      hover
      bordered
      striped
      sticky-header="60vh"
      @row-hovered="mouseOn"
      @row-unhovered="mouseOff"
    >
      <template #cell(reportedDate)="data">
        {{ data.item.reportedDate.substring(0, 10) }}
      </template>
      <template #cell(inspectedDate)="data">
        {{
          data.item.inspectedDate == null
            ? data.item.inspectedDate
            : data.item.inspectedDate.substring(0, 10)
        }}
      </template>
      <template #cell(repairedDate)="data">
        {{
          data.item.repairedDate == null
            ? data.item.repairedDate
            : data.item.repairedDate.substring(0, 10)
        }}
      </template>
    </b-table>
  </div>
</template>

<script>
export default {
  name: "hole-list",
  props: ["potholes"],
  data() {
    return {
      fields: [
        { key: "id", visible: false },
        { key: "latitude", visible: true },
        { key: "longitude", visible: true },
        { key: "imageLink", visible: false },
        { key: "reportedDate", visible: true },
        { key: "reportingUserId", visible: false },
        { key: "inspectedDate", visible: true },
        { key: "repairedDate", visible: true },
        { key: "repairStatus", visible: true },
        { key: "severity", visible: false },
        { key: "isBig", visible: false },
      ],
    };
  },
  computed: {
    visibleFields() {
      return this.fields.filter((field) => field.visible);
    },
  },
  methods: {
    mouseOn(item) {
      this.$emit("mouse-on-tr", item.id);
    },
    mouseOff(item) {
      this.$emit("mouse-off-tr", item.id);
    },
  },
};
</script>

<style>
.list-of-potholes {
  margin: 5px;
}

tr > th {
  text-transform: none;
}
</style>