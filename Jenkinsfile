pipeline {
    agent {
        docker { image 'mcr.microsoft.com/dotnet/core/sdk:latest' }
    }
    stages {
        stage('Build') {
            steps {
                sh 'dotnet restore'
                sh 'dotnet build'
            }
        }
    }
}

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
        stage('Test') {
            agent {
                docker { image 'mcr.microsoft.com/dotnet/core/sdk:latest' }
            }
            steps {
                sh 'dotnet restore'
                sh 'dotnet test'
            }
        }
    }
}