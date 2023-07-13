<template>
  <v-dialog :value="active" persistent >
    <template v-slot:activator="{ on }">
      <v-btn depressed @click="toggleActivity">
        Upload
      </v-btn>
    </template>
        <v-stepper v-model="step">

          <v-stepper-header>
            <v-stepper-step :complete="step > 1" step="1">
              Select Type
            </v-stepper-step>

            <v-divider v-if="type === uploadType.TRICK"></v-divider>

            <v-stepper-step v-if="type === uploadType.TRICK" :complete="step > 2" step="2">
              Trick Information
            </v-stepper-step>

            <v-divider></v-divider>

            <v-stepper-step :complete="step > 3" step="3">
              Upload
            </v-stepper-step>

            <v-divider></v-divider>

            <v-stepper-step :complete="step > 4" step="4">
              Submission Information
            </v-stepper-step>

            <v-divider></v-divider>

            <v-stepper-step step="5">
              Review
            </v-stepper-step>
          </v-stepper-header>

          <v-stepper-items>

            <v-stepper-content step="1">
              <div class="d-flex flex-column align-center">
                <v-btn class="my-2" @click="setType({type: uploadType.TRICK})">Trick</v-btn>
                <v-btn class="my-2" @click="setType({type: uploadType.SUBMISSION})">Submission</v-btn>
              </div>
            </v-stepper-content>

            <v-stepper-content step="2">
              <div>
                <v-text-field label="Trick Name" v-model="trickName"></v-text-field>
                <v-btn @click="incStep">Save Trick</v-btn>
              </div>
            </v-stepper-content>

            <v-stepper-content step="3">
              <div>
                <v-file-input accept="video/*" @change="handleFile" ></v-file-input>
              </div>
            </v-stepper-content>

            <v-stepper-content step="4">
              <div>
                <v-text-field label="Description" v-model="submission"></v-text-field>
                <v-btn @click="incStep">Save submission</v-btn>
              </div>
            </v-stepper-content>

            <v-stepper-content step="5">
              <div>
                  <v-btn @click="save">Save</v-btn>
              </div>
            </v-stepper-content>
          </v-stepper-items>
        </v-stepper>
        <div class="d-flex justify-center my-3" >
          <v-btn @click="toggleActivity">
            Close
            </v-btn>
        </div>
      </v-dialog>
</template>

<script >
import {mapState, mapMutations, mapActions} from 'vuex';
import {UPLOAD_TYPE} from '@/data/enum'
export default {
  data: () => ({
    trickName: "",
    submission: ""
  }),
  computed: {
    ...mapState('tricks', ['tricks']),
    ...mapState('video-upload', ['uploadPromise', 'active', 'step', 'type']),
    uploadType () {
      return UPLOAD_TYPE;
    }
  },
  methods:{
    ...mapMutations('tricks',{
      resetTricks: 'reset'
    }),
    ...mapMutations('video-upload', ['reset','toggleActivity', 'setType', 'incStep']),
    ...mapActions('video-upload', ['startVideoUpload', 'createTrick' ]),

    async handleFile(file) {
      if(!file) return;

      const form = new FormData();
      form.append("video", file);

      console.log(file);
      this.startVideoUpload({form});
    },
    async save() {
      if(!this.uploadPromise)
      {
        console.log("error on uploadPromise:")
        return;
      }
      const video = await this.uploadPromise;
      await this.createTrick({
        trick: {name: this.trickName},
        submission: { description: this.submission,  video, trickId: 1 }
      });
      this.trickName = "";
      this.submission = "";
      this.reset();
    }
  }
}
</script>
