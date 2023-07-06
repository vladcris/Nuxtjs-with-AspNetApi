import  axios  from 'axios'
const initState = () => ({
  message: "init"
})

export const state = initState;

export const mutations = {
  setMessage(state, message) {
    state.message = message;
  },
  reset(state) {
    Object.assign(state, initState());
  }
}

export const actions = {
  async asyncFetchMessage({commit}) {
    const message = (await axios.get('http://localhost:5007/api/home')).data;
    commit("setMessage", message);
  }
}
