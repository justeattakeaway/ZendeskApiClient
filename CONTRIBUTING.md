# Contributing to ZendeskApiClient

## Continuous Integration

1. Create a branch, make required code changes, add unit tests and increment the [version number](https://github.com/justeattakeaway/ZendeskApiClient/blob/master/src/ZendeskApi.Build/ZendeskApi.Commons.props). We aim to follow [Semantic Versioning](https://semver.org/) guidelines within this library. 
2. Create a pull request from `your_branch` -> `develop` branch
   * This will trigger a github actions workflow named *ci*; this builds, runs unit tests, and packages the branch. Once approved, merge your PR into `develop` branch.
3. A pull request from `develop` -> `master` branch will need to be made (note that only repo owners can merge into `master`).
   * This will trigger a github actions workflow named *ci* and will run the run-integration-tests job; tests are run on against a development Zendesk instance which belongs to Just Eat Takeaway.
   * When the integration tests have passed from your pull request and its been approved you can merge.
4. Releasing a new version is a manual step, to be performed by an owner. The new release should appear under the [releases section](https://github.com/justeattakeaway/ZendeskApiClient/releases) of github

## License

By contributing to this project, you agree that your contributions will be licensed under the [Project License](/License.md).

Thank you for your contributions to ZendeskApiClient!
