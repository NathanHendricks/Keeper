import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  account: {},
  profiles: [],
  profile: null,
  keeps: [],
  keep: null,
  vaults: [],
  vault: null,
  vaultkeeps: [],
  vaultkeep: null,
  userskeeps: [],
  userskeep: null,
  uservaults: [],
  uservault: null
})
