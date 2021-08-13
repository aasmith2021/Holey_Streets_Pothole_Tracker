import axios from 'axios';

export default {
    list() {
        return axios.get('/claimform');
    },

    submitClaimForm(claimForm) {
        return axios.post('/claimform/submit', claimForm);
    },

    updateClaimStatus(claim) {
        return axios.put('/claimform', claim)
    }
}