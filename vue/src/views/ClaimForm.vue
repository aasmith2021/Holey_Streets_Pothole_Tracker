<template>
  <div>
    <div class="title-and-pictures">
      <img
        class="left-pic tire-pic"
        src="../../public/tire_track_left.jpg"
        alt="Tire track picture"
      />
      <h1 class="centered-h1 title">Claim Form</h1>
      <img
        class="right-pic tire-pic"
        src="../../public/tire_track_right.jpg"
        alt="Tire track picture"
      />
    </div>

    <div class="subtitle-box-claim-form">
      <p class="subtitle-claim-form">
        <em>Enter your information below to file a damage claim.</em>
      </p>
      <p class="subtitle-claim-form">
        <em
          >Your claim will be reviewed by a Holey Streets administrator, and
          they will contact you with next steps in the claims process.</em
        >
      </p>
    </div>

    <form
      class="claim-form"
      v-if="$store.state.token != ''"
      v-on:submit.prevent="submitClaimForm"
    >
      <div class="names form-block">
        <div class="form-element">
          <label for="firstName">First Name:</label><br />
          <input
            type="text"
            id="firstName"
            name="firstName"
            v-model="claimForm.firstName"
            required
          />
        </div>
        <div class="form-element">
          <label for="lastName">Last Name:</label><br />
          <input
            type="text"
            id="lastName"
            name="lastName"
            v-model="claimForm.lastName"
            required
          />
        </div>
      </div>
      <div class="contact-info form-block">
        <div class="form-element">
          <label for="email">Email:</label><br />
          <input
            type="email"
            id="email"
            name="email"
            v-model="claimForm.email"
            required
          />
        </div>
        <div class="form-element">
          <label for="phoneNumber">Phone Number:</label><br />
          <input
            type="tel"
            id="phoneNumber"
            name="phoneNumber"
            v-model="claimForm.phoneNumber"
            required
          />
        </div>
      </div>
      <div class="dateOfIncident form-block">
        <div class="form-element">
          <label for="locationOfincidentCity">Damage Location (City):</label
          ><br />
          <input
            type="text"
            id="locationOfincidentCity"
            name="locationOfincidentCity"
            v-model="claimForm.locationOfincidentCity"
            required
          />
        </div>
        <div class="form-element">
          <label for="locationOfincidentState">Damage Location (State):</label
          ><br />
          <select
            id="locationOfincidentState"
            name="locationOfincidentState"
            v-model="claimForm.locationOfincidentState"
            required
          >
            <option>AL</option>
            <option>AK</option>
            <option>AZ</option>
            <option>AR</option>
            <option>CA</option>
            <option>CO</option>
            <option>CT</option>
            <option>DE</option>
            <option>FL</option>
            <option>GA</option>
            <option>HI</option>
            <option>ID</option>
            <option>IL</option>
            <option>IN</option>
            <option>IA</option>
            <option>KS</option>
            <option>KY</option>
            <option>LA</option>
            <option>ME</option>
            <option>MD</option>
            <option>MA</option>
            <option>MI</option>
            <option>MN</option>
            <option>MS</option>
            <option>MO</option>
            <option>MT</option>
            <option>NE</option>
            <option>NV</option>
            <option>NH</option>
            <option>NJ</option>
            <option>NM</option>
            <option>NY</option>
            <option>NC</option>
            <option>ND</option>
            <option>OH</option>
            <option>OK</option>
            <option>OR</option>
            <option>PA</option>
            <option>RI</option>
            <option>SC</option>
            <option>SD</option>
            <option>TN</option>
            <option>TX</option>
            <option>UT</option>
            <option>VT</option>
            <option>VA</option>
            <option>WA</option>
            <option>WV</option>
            <option>WI</option>
            <option>WY</option>
          </select>
        </div>
      </div>
      <div class="form-block">
        <div class="form-element date-of-damage">
          <label for="dateOfIncident">Date of Damage:</label
          ><br class="date-of-damage-br" />
          <b-form-datepicker
            :max="max"
            id="dateOfIncident"
            name="dateOfIncident"
            v-model="claimForm.dateOfIncident"
            required
          />
        </div>
        <div class="form-element picture-link">
          <label for="imageLink">Link to Picture of Damage (optional):</label
          ><br class="picture-link-break" />
          <input
            type="text"
            id="imageLink"
            name="imageLink"
            v-model="claimForm.imageLink"
          />
        </div>
      </div>
      <div class="form-element damage-description">
        <label class="description-of-damage" for="descriptionOfDamage"
          >Please enter a detailed description of the damage caused by a pothole
          <em>(max. 1200 characters)</em>:</label
        >
        <textarea
          id="descriptionOfDamage"
          name="descriptionOfDamage"
          v-model="claimForm.descriptionOfDamage"
        />
      </div>
      <button type="submit" class="submit-button">Submit Claim</button>
    </form>
  </div>
</template>

<script>
import claimFormService from "../services/ClaimFormService.js";

export default {
  name: "claim-form",
  data() {
    const now = new Date();
    const today = new Date(now.getFullYear(), now.getMonth(), now.getDate());
    const maxDate = new Date(today);
    return {
      claimForm: {
        firstName: "",
        lastName: "",
        email: "",
        phoneNumber: "",
        dateOfClaim: "",
        dateOfIncident: "",
        locationOfincidentCity: "",
        locationOfincidentState: "",
        imageLink: "",
        descriptionOfDamage: "",
      },
      max: maxDate,
    };
  },
  methods: {
    submitClaimForm() {
      const today = new Date();
      this.claimForm.dateOfClaim = today.toJSON();
      console.log("bananas");
      claimFormService
        .submitClaimForm(this.claimForm)
        .then((response) => {
          if (response.status === 200) {
            alert("Thank you! We will never get back to you about this.");
          }
          this.claimForm = {};
        })
        .catch((error) => {
          this.handleError(error);
        });
    },
    methods: {
      submitClaimForm() {
        const today = new Date();
        this.claimForm.dateOfClaim = today.toJSON();

        claimFormService
          .submitClaimForm(this.claimForm)
          .then((response) => {
            if (response.status === 200) {
              alert("Thank you! We will never get back to you about this.");
            }
            this.claimForm = {};
          })
          .catch((error) => {
            this.handleError(error);
          });
      },
      handleError(error) {
        console.log(error);
      },
    },
  },
};
</script>

<style scoped>
@media only screen and (min-width: 600px) {
  form {
    display: flex;
    flex-direction: column;
    gap: 20px;
    width: 98vw;
    align-items: center;
  }

  div.form-block {
    display: flex;
    flex-direction: row;
    gap: 20px;
  }

  #imageLink {
    width: 300px;
  }

  div.damage-description {
    display: flex;
    flex-direction: column;
    align-items: center;
  }

  .description-of-damage {
    width: 42vw;
  }

  textarea {
    width: 42vw;
    height: 6vw;
  }
}

@media only screen and (max-width: 600px) {
  form {
    display: flex;
    flex-direction: column;
    gap: 20px;
    width: 98vw;
    align-items: center;
  }

  .description-of-damage {
    width: 17rem;
    text-align: center;
  }

  div.damage-description {
    display: flex;
    flex-direction: column;
    align-items: center;
  }

  div.form-element:nth-child(1) {
    margin-bottom: 15px;
  }

  div.form-element:nth-child(3) {
    margin-top: 15px;
  }

  div.picture-link {
    display: flex;
    flex-direction: column;
    align-items: center;
    gap: 0;
  }

  div.date-of-damage {
    display: flex;
    flex-direction: column;
    align-items: center;
    gap: 0;
  }

  br.date-of-damage-br {
    display: none;
  }

  br.picture-link-break {
    display: none;
  }
}

button.submit-button {
  width: 150px;
  margin-bottom: 20px;
  background-color: #acd178;
  border: 1px solid black;
  border: 1px solid black;
  border-radius: 5px;
}
</style>