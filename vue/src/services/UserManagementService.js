import axios from 'axios';

export default {
    getListOfRequests() {
        return axios.get('/usermanagement/request');
    },

    approveRequest(userId) {
        return axios.put(`/usermanagement/request/${userId}?approved=true`);
    },

    denyRequest(userId) {
        return axios.put(`/usermanagement/request/${userId}?approved=false`);
    },

    requestEmployment() {
        return axios.post(`/usermanagement/request`)
    },

    getListOfEmployees() {
        return axios.get('/usermanagement/employee');
    },

    removeEmployeeRole(userId) {
        return axios.put(`/usermanagement/${userId}?newrole=user`);
    },

    getAllNonEmployeeUsers() {
        const userRole = 'user';

        return axios.get(`/usermanagement/${userRole}`);
    }
}