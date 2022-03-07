pipeline {
    agent {
        label 'ecs-agent'
    }
    options {
            timeout(time: 30, unit: 'MINUTES')
            buildDiscarder(logRotator(numToKeepStr: '5', artifactNumToKeepStr: '5'))
    }
    environment {
        IMAGE = """${sh(
                returnStdout: true,
                script: 'echo  ${NEW_ALI_REGISTRY}"/masa/masa-blazor-docs:0.2."${BUILD_ID}'
            )}"""
        IMAGE_PRD =  """${sh(
                returnStdout: true,
                script: 'echo  ${NEW_ALI_REGISTRY}"/masa/masa-blazor-docs:"${GIT_BRANCH}'
            )}"""
        NEW_ALI_REGISTRY_AUTH = credentials('NEW_ALI_REGISTRY_AUTH')
        KUBE_CONFIG_DEV = credentials('k8s-ack')
        KUBE_CONFIG_PRD = credentials('k8s-ack-prd')
        NUGET_KEY = credentials('nuget-key')
    }
    stages {
        stage('packer-dev') {
            options {
                retry (2)
            }
            when {
                branch 'develop'
            }
            steps{
                sh '''
                    ls src/
                    node -v
                    dotnet --version
                    git clone -b develop https://github.com/BlazorComponent/BlazorComponent.git ./src/BlazorComponent
                    dotnet build src
                    dotnet pack src --include-symbols -p:PackageVersion=0.2."${BUILD_ID}"
                    dotnet nuget push "**/*.symbols.nupkg" -k $NUGET_KEY -s https://api.nuget.org/v3/index.json
                    '''                    
            }
        }
        stage('docker-dev') {
            options {
                retry (2)
            }
            when {
                branch 'develop'
            } 
            steps {
                sh '''
                    docker login $NEW_ALI_REGISTRY --username=$NEW_ALI_REGISTRY_AUTH_USR -p $NEW_ALI_REGISTRY_AUTH_PSW
                    docker build -t $IMAGE .
                    docker push $IMAGE
                    docker rmi  $IMAGE
                    '''
            }    
        }
        stage('deploy-dev') {
            when {
                branch 'develop'
            }
            steps {
                sh '''
                    echo $KUBE_CONFIG_DEV | base64 -d > ./config
                    kubectl --kubeconfig ./config set image deployment/masa-blazor-docs masa-blazor-docs=$IMAGE -n masa-blazor
                    '''
            }
        }
        stage('deploy-test') {
            when {
                branch 'develop'
            }
            steps {
                sh '''
                    echo $KUBE_CONFIG_DEV | base64 --decode >> ./config
                    kubectl --kubeconfig ./config set image deployment/masa-blazor-docs masa-blazor-docs=$IMAGE -n masa-blazor
                    '''
            }
        }
        stage('packer-prd') {
            options {
                retry (2)
            }
            when {
                buildingTag()
            }
            steps{
                sh '''
                    ls src/
                    node -v
                    dotnet --version
                    git clone -b develop https://github.com/BlazorComponent/BlazorComponent.git ./src/BlazorComponent
                    dotnet build src
                    dotnet pack src --include-symbols -p:PackageVersion=${GIT_BRANCH}"
                    dotnet nuget push "**/*.symbols.nupkg" -k $NUGET_KEY -s https://api.nuget.org/v3/index.json
                    '''
            }
        }
        stage('docker-prd') {
            options {
                retry (2)
            }
            when {
                buildingTag()
            } 
            steps {     
                 sh '''
                    docker login $NEW_ALI_REGISTRY --username=$NEW_ALI_REGISTRY_AUTH_USR -p $NEW_ALI_REGISTRY_AUTH_PSW
                    docker build -t $IMAGE_PRD .
                    docker push $IMAGE_PRD
                    docker rmi  $IMAGE_PRD
                    '''         
            }    
        }
        stage('deploy-prd') {
            when {
                buildingTag()
            }
            steps {
                sh '''
                    echo $KUBE_CONFIG_PRD | base64 --decode >> ./config
                    kubectl --kubeconfig ./config set image deployment/masa-blazor-docs masa-blazor-docs=$IMAGE -n masa-blazor
                    '''       
            }
        }
    }
}
