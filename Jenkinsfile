pipeline {
    def app

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
        stage('Build Container') {
            app = docker.build("robsmithw/shoppingtrackapi")
        }
        stage('Push Container') {
            docker.withRegistry('https://registry.hub.docker.com', 'docker-hub'){
                app.push("latest")
            }
        }
    }
}