#!/bin/bash

curl -u "token-c6r6m:lxmg65pvpkw94c4x867vmmzlqvfwf7n9z8xk6k4tsnkzn4v9mdh484" \
-X POST \
-H 'Accept: application/json' \
-H 'Content-Type: application/json' \
'https://rancher-hz.lonsid.cn/v3/project/local:p-f59cg/workloads/deployment:dev-techs-preview:masa-blazor-docs?action=redeploy'
