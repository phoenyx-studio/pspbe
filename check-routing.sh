#\!/bin/bash
echo "Checking for potential routing conflicts..."
conflicts=$(grep -rn "Route.*{.*}" Controllers/  < /dev/null |  grep -v "Order")
if [ -n "$conflicts" ]; then
    echo "Potential routing conflicts found:"
    echo "$conflicts"
    echo "Add 'Order = 2' to fix these routes."
    exit 1
else
    echo "No routing conflicts detected\!"
fi
echo "Running routing tests..."
cd Tests && dotnet test --filter RoutingConflictTests --verbosity minimal
