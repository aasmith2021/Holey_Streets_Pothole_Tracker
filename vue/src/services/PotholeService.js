import axios from 'axios';

export default {
    list() {
        return axios.get('/pothole/list')
    },

    addPothole(pothole) {
        return axios.post('/pothole/add', pothole)
    },

    deletePothole(id) {
        return axios.delete(`/pothole/${id}`)
    },

    updatePothole(pothole) {
        return axios.put('/pothole', pothole)
    }
}