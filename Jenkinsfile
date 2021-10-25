pipeline {
	agent any
	
	environment {
		BuildConfiguration="Debug"
	}

	stages {
		stage('Build') {
			steps {
				sh "dotnet build -c $BuildConfiguration"
			}
		}

		stage('Test') {
			steps {
				sh 'dotnet test -c $BuildConfiguration --logger trx --no-build'
			}
		}
		
		stage('Pack & Push packages') {
			steps {
				sh 'dotnet pack Tgs.WebApi.Toolkits/Tgs.WebApi.Toolkits.csproj -c $BuildConfiguration --no-build --no-restore'
				sh 'dotnet nuget push Tgs.WebApi.Toolkits/bin/$BuildConfiguration/*.nupkg -s "raypro-baget" -k admin --skip-duplicate'
			}
		}
	}

	post {
        always {
            step([$class: 'MSTestPublisher', testResultsFile:"**/*.trx", failOnError: false, keepLongStdio: true])
        }
	}
}
