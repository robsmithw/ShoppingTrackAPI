pipeline {
    agent none
    
    stages {
        stage('Build') {
            agent {
                docker { image 'mcr.microsoft.com/dotnet/core/sdk:latest' }
            }
            steps {
                sh 'dotnet restore'
                sh 'dotnet build'
            }
        }
        // stage('Test') {
        //     agent {
        //         docker { image 'mcr.microsoft.com/dotnet/core/sdk:latest' }
        //     }
        //     steps {
        //         sh 'dotnet restore'
        //         sh 'dotnet test'
        //     }
        // }
        stage('Build and Push Container') {
            steps{
                script{
                    def app
                    app = docker.build("robsmithw/shoppingtrackapi")
                    docker.withRegistry('https://registry.hub.docker.com', 'docker-hub'){
                        app.push("latest")
                    }
                }    
            }
        }
    }
}