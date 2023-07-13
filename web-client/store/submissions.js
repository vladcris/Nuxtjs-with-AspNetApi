const initState = () => ({
  submissions: []
})

export const state = initState;

export const mutations = {
  setSubmission(state, {submissions}) {
    state.submissions = submissions;
  },
  reset(state) {
    Object.assign(state, initState())
  }
}

export const actions = {
  async fetchSubmissionsForTrick({commit}, {trickId}) {
    const submissions = await this.$axios.$get(`/api/tricks/${trickId}/submissions`);
    console.log(submissions)
    commit('setSubmission', {submissions});
  }
}
